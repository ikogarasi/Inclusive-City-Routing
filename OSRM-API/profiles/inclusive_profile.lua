local profile = {}

profile.properties = {
    weight_name = 'inclusive',
    max_speed_for_map_matching = 15,
    u_turn_penalty = 120,
    continue_straight_at_waypoint = false,
    use_turn_restrictions = true,
    left_hand_driving = false,
    traffic_signal_penalty = 15,
    speed_reduction = 1
}

function calculate_weight_by_tags(way)
    local weight = 1.0

    local access = way:get_value_by_key("access")
    local foot = way:get_value_by_key("foot")
    if access == "private" or foot == "private" or (access == "no" and foot ~= "yes") then
        return nil
    end

    if way:get_value_by_key("construction") or way:get_value_by_key("fixme") then
        return nil
    end

    local ramp = way:get_value_by_key("ramp")
    local highway = way:get_value_by_key("highway")
    local surface = way:get_value_by_key("surface")
    local smoothness = way:get_value_by_key("smoothness")
    local source = way:get_value_by_key("source")

    if highway == "steps" then
        if ramp == "yes" then
            weight = weight + 0.8
        else
            return nil
        end
    end

    if (highway == "path" or highway == "footway") and (surface == "grass" or surface == "dirt" or surface == "ground") then
        return nil
    end

    if highway == "elevator" or way:get_value_by_key("elevator") == "yes" then
        weight = weight - 1.5
    end

    local tunnel = way:get_value_by_key("tunnel")
    if (tunnel == "passage" or tunnel == "subway") and ramp ~= "yes" and highway ~= "elevator" then
        weight = weight + 1.5
    end

    local wheelchair = way:get_value_by_key("wheelchair")
    if wheelchair == "no" then
        return nil
    elseif wheelchair == "yes" then
        weight = weight - 1.0
    end

    local width = tonumber(way:get_value_by_key("width"))
    if width and width < 1.2 then
        weight = weight + 1.0
    elseif width and width >= 1.8 then
        weight = weight - 0.3
    end

    local tactile_paving = way:get_value_by_key("tactile_paving")
    if tactile_paving == "yes" then
        weight = weight - 0.3
    end

    if smoothness == "excellent" then
        weight = weight - 0.5
    elseif smoothness == "bad" or smoothness == "very_bad" then
        weight = weight + 1.0
    end

    local incline = tonumber(way:get_value_by_key("incline"))
    if incline and math.abs(incline) > 6 then
        weight = weight + 1.0
    end

    local slope = tonumber(way:get_value_by_key("slope"))
    if slope and slope > 6 then
        weight = weight + ((slope - 6) * 0.2)
    end

    if ramp == "yes" then
        weight = weight - 1.0
    elseif ramp == "no" then
        weight = weight + 1.0
    end

    local kerb = way:get_value_by_key("kerb")
    if kerb == "raised" or kerb == "unknown" then
        weight = weight + 1.5
    elseif kerb == "no" or kerb == "flush" or kerb == "lowered" then
        weight = weight - 0.5
    elseif kerb == "yes" then
        weight = weight + 0.5
    end

    local crossing = way:get_value_by_key("crossing")
    if crossing == "traffic_signals" then
        weight = weight - 0.3
    elseif crossing == "uncontrolled" then
        weight = weight + 0.7
    end

    local bad_surfaces = {
        gravel = 4.0,
        dirt = 4.0,
        grass = 4.5,
        sand = 5.0,
        ground = 2.5,
        mud = 4.0
    }

    if bad_surfaces[surface] then
        weight = weight + bad_surfaces[surface]
    elseif surface == "wood" then
        weight = weight + 0.5
    elseif surface == "cobblestone" then
        weight = weight + 0.8
    elseif surface == "concrete" then
        weight = weight
    end

    local sidewalk = way:get_value_by_key("sidewalk")
    if not sidewalk or sidewalk == "no" then
        weight = weight + 2.0
    end

    if source == "survey" and not surface and not smoothness and not incline and not ramp then
        weight = weight + 2.5
    end

    return weight
end

function way_function(way, result)
    local highway = way:get_value_by_key("highway")
    if not highway then
        return
    end

    local weight = calculate_weight_by_tags(way)
    if not weight then
        return
    end

    result.forward_mode = mode.walking
    result.backward_mode = mode.walking
    result.weight = weight
    result.forward_speed = 5
    result.backward_speed = 5
end

profile.process_way = way_function

return profile
