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
