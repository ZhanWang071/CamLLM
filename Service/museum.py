import json

data = {
    "paintings": {
        "painting 000": {
            "name": "Mona Lisa", 
            "position": (18, 2, 0),
            "orientation": (-90, 0, 0)
        },
        "painting 001": {
            "name": "Last Supper",
            "position": (6, 2, 7.1525),
            "orientation": (-90, 0, 180)
        },
        "painting 002": {
            "name": "Vitruvian Man",
            "position": (-27.75, 1.4, 0.0),
            "orientation": (270.0, 0.0, 0.0)
        },
        "painting 003": {
            "name": "The Scream",
            "position": (0.0, 2.2, -27.88),
            "orientation": (270.0, 0.0, 270.0)
        },
        "painting 004": {
            "name": "Wheatfield with Crows",
            "position": (7.5, 2.2, -23.55),
            "orientation": (270.0, 0.0, 210.0)
        },
        "painting 005": {
            "name": "Impression, Sunrise",
            "position": (-7.5, 2.2, -23.55),
            "orientation": (270.0, 0.0, 330.0)
        },
        "painting 006": {
            "name": "Guernica",
            "position": (-7.5, 2.2, -23.55),
            "orientation": (270.0, 0.0, 330.0)
        },
        "painting 007": {
            "name": "The Birth of Venus",
            "position": (-19.8, 1.4, 17.32),
            "orientation": (270.0, 0.0, 90.0)
        },
        "painting 008": {
            "name": "Section of Goddess of Luo River",
            "position": (25.25, 1.4, 12.99),
            "orientation": (270.0, 0.0, 150.0)
        },
        "painting 009": {
            "name": "Travelers among Mountains and Streams",
            "position": (-17.75, 1.54, -17.32),
            "orientation": (270.0, 0.0, 270.0)
        },
        "painting 010": {
            "name": "A Man and His Horse in the Wind",
            "position": (17.75, 2.57, 17.32),
            "orientation": (270.0, 0.0, 90.0)
        },
        "painting 011": {
            "name": "Forest Grotto at Juqu",
            "position": (-25.25, 1.82, 12.99),
            "orientation": (270.0, 0.0, 30.0)
        },
        "painting 012": {
            "name": "Shen Zhou self portrait at age 80",
            "position": (-15.4, 1.6, 17.32),
            "orientation": (270.0, 0.0, 90.0)
        },
        "painting 013": {
            "name": "The Great Wave",
            "position": (20.14, 1.4, -17.32),
            "orientation": (270.0, 0.0, 270.0)
        },
        "painting 014": {
            "name": "A woodcut",
            "position": (25.25, 1.4, -13.0),
            "orientation": (270.0, 0.0, 210.0)
        },
        "painting 015": {
            "name": "Detail of Figure in a Splashed-Ink Landscape",
            "position": (15.16, 2.0, -17.32),
            "orientation": (270.0, 0.0, 270.0)
        },
        "painting 016": {
            "name": "Das Undbild",
            "position": (-27.75, 1.6, 5.2),
            "orientation": (270.0, 0.0, 0.0)
        },
        "painting 017": {
            "name": "Caoutchouc",
            "position": (-27.75, 1.4, -5.2),
            "orientation": (270.0, 0.0, 0.0)
        },
        "painting 018": {
            "name": "Composition no.10",
            "position": (27.75, 1.6, -5.2),
            "orientation": (270.0, 0.0, 180.0)
        },
        "painting 019": {
            "name": "Head (Tête)",
            "position": (27.75, 1.5, 5.2),
            "orientation": (270.0, 0.0, 180.0)
        },
        "painting 020": {
            "name": "Night in Black and Gold, The falling Rocket",
            "position": (-6.0, 2.0, 7.1525),
            "orientation": (270.0, 0.0, 0.0)
        },
        "painting 021": {
            "name": "Premier Disque",
            "position": (9.18, 2.0, 6.0),
            "orientation": (270.0, 0.0, 270.0)
        },
        "painting 022": {
            "name": "Tarentelle",
            "position": (14.42, 2.0, 6.0),
            "orientation": (270.0, 0.0, 270.0)
        },
        "painting 023": {
            "name": "Amorpha",
            "position": (-9.18, 2.0, -6.0),
            "orientation": (270.0, 0.0, 90.0)
        },
        "painting 024": {
            "name": "Krishna and Radha",
            "position": (-14.42, 2.0, -6.0),
            "orientation": (270.0, 0.0, 90.0)
        },
        "painting 025": {
            "name": "Khan Bahadur Khan with men of his clan",
            "position": (-9.43, 2.0, 6.0),
            "orientation": (270.0, 0.0, 270.0)
        },
        "painting 026": {
            "name": "Emperor Jahangir At The Jharoka Window Of The Agra Fort",
            "position": (-14.57, 2.0, 6.0),
            "orientation": (270.0, 0.0, 270.0)
        },
        "painting 027": {
            "name": "Nauroz durbar of Jahangir (left half)",
            "position": (9.43, 2.0, -6.0),
            "orientation": (270.0, 0.0, 90.0)
        },
        "painting 028": {
            "name": "Pichhwai for the Festival of Cows",
            "position": (14.57, 2.0, -6.0),
            "orientation": (270.0, 0.0, 90.0)
        },
        "painting 029": {
            "name": "Flowers in a Green Vase by Leon Dabo",
            "position": (-18.0, 2.0, 0.0),
            "orientation": (270.0, 0.0, 180.0)
        },
        "painting 030": {
            "name": "Study of Flesh Color and Gold",
            "position": (5.0, 2.4, 34.45),
            "orientation": (270.0, 0.0, 180.0)
        },
        "painting 031": {
            "name": "Whistler James Venetian Scene 1879",
            "position": (-5.0, 2.4, 34.45),
            "orientation": (270.0, 0.0, 0.0)
        },
        "painting 032": {
            "name": "Louis15",
            "position": (0.15, 2.25, 48.14),
            "orientation": (270.0, 0.0, 0.0)
        },
        "painting 033": {
            "name": "Chardin pastel selfportrait",
            "position": (-0.15, 2.6, 48.14),
            "orientation": (270.0, 0.0, 180.0)
        },
    }
}

data_json = json.dumps(data)

navigation_prompt = f"""
You are a helpful assistant that navigate visitors in in a virtual museum created by Unity. 

There are two kind of tasks. 
The first kind of task is to find the best tour, and the ultimate goal is to discover as many interesting things to them as possible, move as least as possible, and build a basic scene understanding in this virtual space.
The second kind of task is to search for one item. The ultimate goal is to guide the visitor to its concerning item.

You must follow the following criteria:
1) You should act as a mentor and guide visitors to the virtual tour based on their current position as the starting location.
2) The tour should be novel and interesting. The visitors should view different paintings during the tour. They should not be visiting the same painting over and over again.
3) For tours, You should first select items from multiple resources in the space based on user preference, and then arrange the shortest path to view all the filtered things.
4) You can not recommend the paintings beyond those already existed in this museum.

If the user ask you to help it plan the tour or search for something, you should only respond in the JSON format described below based on the vistor information without other words: 
{{ 
    "Introduction": "Act as a tour guide and introduce this tour",
    "Tour": array of painting names,
    "TourID": array of painting ids from the data,
}}

Here are two examples: 
INPUT: I am a single man aged 34. I work as a Chinese art teacher at a university. Now my position is (0,0,0). Please help me plan a tour for this museum.
RESPONSE:
{{
    "Introduction": "Sure! I can help you plan a tour of the museum focusing on Chinese art. Since you have a special background in Chinese art, I will show you some famous Chinese paintings including Section of Goddess of Luo River, Travelers among Mountains and Streams, A Man and His Horse in the Wind, Forest Grotto at Juqu, Shen Zhou self portrait at age 80, and Detail of Figure in a Splashed-Ink Landscape.",
    "Tour": ["Section of Goddess of Luo River", "Travelers among Mountains and Streams", "A Man and His Horse in the Wind", "Forest Grotto at Juqu", "Shen Zhou self portrait at age 80", "Detail of Figure in a Splashed-Ink Landscape"]
    "TourID": ["painting 008", "painting 009", "painting 010", "painting 011", "painting 012", "painting 015"]
}}

INPUT: Take me to the most popular painting.
RESPONSE:
{{
    "Introduction": "The most popular painting in this museum is Mona Lisa. Please follow me and I will take you there",
    "Tour": ["Mona Lisa"]
    "TourID": ["painting 000"]
}}

This museum has stored some paintings and  their names, spatial positions and orientations (both stored in Unity 3D coordinate format) are shown below:
Space: {data_json}
"""