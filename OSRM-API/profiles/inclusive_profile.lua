local profile = {}

-- OSRM expects this to be set
profile.properties = {
    weight_name = 'inclusive',
    max_speed_for_map_matching = 110,
    u_turn_penalty = 20,
    continue_straight_at_waypoint = false,
    use_turn_restrictions = false,
    left_hand_driving = false,
    traffic_signal_penalty = 2,
    speed_reduction = 0.8
}

function calculate_weight_by_tags(way)
    local weight = 1

    local gradient = tonumber(way:get_value_by_key("slope"))
    if gradient then
        local gradient_penalty = math.max(gradient - 6, 0) * 0.1
        weight = weight + gradient_penalty
    end

    local wheelchair_accessible = way:get_value_by_key("wheelchair")
    if wheelchair_accessible == "no" then
        weight = weight + 0.5
    elseif wheelchair_accessible == "yes" then
        weight = weight - 0.2
    end

    local width = tonumber(way:get_value_by_key("width"))
    if width and width < 1.2 then
        weight = weight + 0.5
    end

    local tactile_paving = way:get_value_by_key("tactile_paving")
    if tactile_paving == "yes" then
        weight = weight - 0.1
    end

    local smoothness = way:get_value_by_key("smoothness")
    if smoothness == "excellent" then
        weight = weight - 0.2
    elseif smoothness == "bad" or smoothness == "very_bad" then
        weight = weight + 0.5
    end

    local incline = tonumber(way:get_value_by_key("incline"))
    if incline and math.abs(incline) > 6 then
        weight = weight + 0.3
    end

    local crossing = way:get_value_by_key("crossing")
    if crossing == "traffic_signals" then
        weight = weight - 0.1
    elseif crossing == "uncontrolled" then
        weight = weight + 0.2
    end
    
    local ramp = way:get_value_by_key("ramp")
    if ramp == "yes" then
        weight = weight - 0.3
    elseif ramp == "no" then
        weight = weight + 0.5
    end
    
    local step_count = tonumber(way:get_value_by_key("step_count"))
    if step_count and step_count > 0 then
        weight = weight + 1.0
    end

    local kerb = way:get_value_by_key("kerb")
    if kerb == "raised" or kerb == "unknown" then
        weight = weight + 0.7
    elseif kerb == "no" or kerb == "flush" or kerb == "lowered" then
        weight = weight - 0.2
    elseif kerb == "yes" then
        weight = weight + 0.3
    end

    local surface = way:get_value_by_key("surface")
    if surface == "concrete" then
        weight = weight + 0.1
    elseif surface == "cobblestone" then
        weight = weight + 0.4
    elseif surface == "gravel" then
        weight = weight + 0.5
    elseif surface == "sand" then
        weight = weight + 0.7
    elseif surface == "dirt" then
        weight = weight + 0.5
    elseif surface == "wood" then
        weight = weight + 0.2
    end

    local sidewalk = way:get_value_by_key("sidewalk")
    if not sidewalk or sidewalk == "no" then
        weight = weight + 0.7
    end

    return weight
end

function way_function(way, result)
    local highway = way:get_value_by_key("highway")
    if not highway then
        return
    end

    -- Assign weight (lower is better)
    local weight = calculate_weight_by_tags(way)

    -- Assign a fallback speed so OSRM keeps the edge
    local speed = 10  -- in km/h

    result.forward_mode = mode.walking
    result.backward_mode = mode.walking

    -- If you're using custom weights:
    result.weight = weight

    -- OSRM still requires a speed value for routing
    result.forward_speed = speed
    result.backward_speed = speed
end

profile.process_way = way_function

return profile
