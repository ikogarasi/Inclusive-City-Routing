import os
import psycopg2

DB_CONFIG = {
    'dbname': os.environ.get('POSTGRES_DB', 'osrm_routes'),
    'user': os.environ.get('POSTGRES_USER', 'osrm_user'),
    'password': os.environ.get('POSTGRES_PASSWORD', 'Pas3w0rd'),
    'host': os.environ.get('POSTGRES_HOST', 'localhost'),
    'port': os.environ.get('POSTGRES_PORT', '5432')
}

output_file = "/data/custom_weights.lua"

QUERY = """
SELECT way_id, sp_get_inclusive_weights(way_id) AS weight
FROM inclusive_weights
"""

def generate_lua_weight_file():
    conn = psycopg2.connect(**DB_CONFIG)
    cur = conn.cursor()
    cur.execute(QUERY)

    rows = cur.fetchall()

    with open(output_file, 'w') as fw:
        fw.write("return {\n")
        for way_id, weight in rows:
            if weight is not None:
                fw.write(" [{}] = {:.3f},\n".format(way_id, weight))
        fw.write("}\n")
    
    print("Wrote {} entries to {}".format(len(rows), output_file))

    cur.close()
    conn.close()

if __name__ == "__main__":
    generate_lua_weight_file()
