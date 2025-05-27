local profile = {}

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
    local weight = 1.0

    -- Бар’єри
    local step_count = tonumber(way:get_value_by_key("step_count"))
    if step_count and step_count > 0 then
        return nil  -- ігнорувати шляхи зі сходами
    end

    local wheelchair = way:get_value_by_key("wheelchair")
    if wheelchair == "no" then
        return nil  -- ігнорувати зовсім недоступні
    elseif wheelchair == "yes" then
        weight = weight - 1.0
    end

    local width = tonumber(way:get_value_by_key("width"))
    if width and width < 1.2 then
        weight = weight + 1.0
    end

    local tactile_paving = way:get_value_by_key("tactile_paving")
    if tactile_paving == "yes" then
        weight = weight - 0.3
    end

    local smoothness = way:get_value_by_key("smoothness")
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

    local ramp = way:get_value_by_key("ramp")
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

    local surface = way:get_value_by_key("surface")
    if surface == "concrete" then
        weight = weight
    elseif surface == "cobblestone" then
        weight = weight + 0.8
    elseif surface == "gravel" then
        weight = weight + 1.0
    elseif surface == "sand" or surface == "dirt" then
        weight = weight + 1.2
    elseif surface == "wood" then
        weight = weight + 0.5
    end

    local sidewalk = way:get_value_by_key("sidewalk")
    if not sidewalk or sidewalk == "no" then
        weight = weight + 2.0
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
        return  -- шлях не підходить для інклюзивного маршруту
    end

    result.forward_mode = mode.walking
    result.backward_mode = mode.walking
    result.weight = weight
    result.forward_speed = 5
    result.backward_speed = 5
end

profile.process_way = way_function

return profile
