ALTER USER osrm_user WITH PASSWORD 'Pas3w0rd';

CREATE TABLE surface_type (
    surface_type_id SERIAL PRIMARY KEY,
    surface_name VARCHAR(50),
    surface_penalty DOUBLE PRECISION DEFAULT 0.0
);

-- Add missing semicolon after CREATE TABLE
INSERT INTO surface_type (surface_name, surface_penalty) VALUES
('asphalt', 0.0),
('concrete', 0.1),
('cobblestone', 0.4),
('gravel', 0.5),
('sand', 0.7),
('dirt', 0.5),
('wood', 0.2);

CREATE TABLE inclusive_weights (
  inclusive_weights_id SERIAL PRIMARY KEY,
  way_id BIGINT UNIQUE NOT NULL, -- OSM way ID for precise matching
  surface_type_id INT REFERENCES surface_type(surface_type_id),
  wheelchair_accessible BOOLEAN DEFAULT TRUE, -- Whether the street is wheelchair accessible
  gradient DOUBLE PRECISION,
  curb_ramps BOOLEAN DEFAULT FALSE, -- Presence of curb ramps
  obstacle_penalty DOUBLE PRECISION DEFAULT 1.0 -- Penalty weight for obstacles (e.g., stairs, narrow paths)
);

CREATE OR REPLACE FUNCTION sp_get_inclusive_weights(way_id_input BIGINT)
RETURNS DOUBLE PRECISION AS $$
DECLARE
    weight DOUBLE PRECISION := 1.0;
    iw RECORD;
    surface_penalty DOUBLE PRECISION;
BEGIN
    SELECT * INTO iw
    FROM inclusive_weights
    WHERE way_id = way_id_input;
    
    IF NOT FOUND THEN
        RETURN NULL;
    END IF;

    IF NOT iw.wheelchair_accessible THEN
        weight := weight + 0.5;
    END IF;

    IF NOT iw.curb_ramps THEN
        weight := weight + 0.3;
    END IF;

    IF iw.gradient IS NOT NULL THEN
        weight := weight + GREATEST(iw.gradient - 6, 0) * 0.1;
    END IF;

    SELECT COALESCE(surface_penalty, 0.0) INTO surface_penalty
    FROM surface_type
    WHERE surface_type_id = iw.surface_type_id;

    weight := weight + surface_penalty;

    weight := weight * iw.obstacle_penalty;

    RETURN weight;
END;
$$ LANGUAGE plpgsql;
