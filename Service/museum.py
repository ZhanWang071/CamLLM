data = {
    "paintings": {
        "painting 000": {
            "name": "Mona Lisa", 
            "position": (18,2,0),
            "orientation": (-90,0,180)
        },
        "painting 001": {
            "name": "Last Supper", 
            "position": (6,2,7.2),
            "orientation": (-90,0,0)
        },
        "painting 002": {
            "name": "Vitruvian Man", 
            "position": (-27.75,2,7.15),
            "orientation": (-90,0,180)
        },
        "painting 003": {
            "name": "The Scream", 
            "position": (0,2.2,-27.8),
            "orientation": (-90,0,-90)
        },
        "painting 004": {
            "name": "Wheatfield with Crows", 
            "position": (7.5,2.2,-23.5),
            "orientation": (-90,0,-60)
        },
        "painting 005": {
            "name": "Impression, Sunrise", 
            "position": (-7.5,2.2,-23/5),
            "orientation": (-90,0,60)
        },
        "painting 006": {
            "name": "Guernica", 
            "position": (-25.25,2,-13),
            "orientation": (-90,0,150)
        },
        "painting 007": {
            "name": "The Birth of Venus", 
            "position": (-19.4,2,17.3),
            "orientation": (-90,0,-90)
        },
        "painting 008": {
            "name": "Section of Godess of Luo River", 
            "position": (25.25,2,13),
            "orientation": (-90,0,-30)
        },
        "painting 009": {
            "name": "Travelers among Mountains and Streams", 
            "position": (-17.75,2,-17.3),
            "orientation": (-90,0,-90)
        },
        "painting 010": {
            "name": "A Man and His Horse in the Wind", 
            "position": (17.75,2,17.32),
            "orientation": (-90,0,90)
        },
        "painting 011": {
            "name": "Forest Grotto at Juqu", 
            "position": (-25.25,2,13),
            "orientation": (-90,0,30)
        },
        "painting 012": {
            "name": "Shen Zhou self portrait at age 80", 
            "position": (-16,2,17.3),
            "orientation": (-90,0,-90)
        },
        "painting 013": {
            "name": "The Great Wave", 
            "position": (19.5,2,-17.3),
            "orientation": (-90,0,90)
        },
        "painting 014": {
            "name": "A woodcut", 
            "position": (25.25,2,-13),
            "orientation": (-90,0,150)
        },
        "painting 015": {
            "name": "Detail of Figure in a Splashed-Ink Landscape", 
            "position": (16,2,-17.3),
            "orientation": (-90,0,90)
        }
    },
        # "door": {"door 1": (0,1,0), "door 2": (0,1,3)}
}

startPrompt = """
You are a helpful assistant that tells visitors the best tour road in a virtual museum created by Unity. The ultimate goal is to discover as many interesting things to them as possible, move as least as possible, and build a basic scene understanding in this virtual space.

You must follow the following criteria:
1) You should act as a mentor and guide visitors to the virtual tour based on their current position as the starting location.
2) The tour should be novel and interesting. The visitors should view different paintings during the tour. They should not be visiting the same painting over and over again.
3) You should first select items from multiple resources in the space based on user preference, and then arrange the shortest path to view all the filtered things.
4) You can not recommend the paintings beyond those already existed in this museum.

If the user ask you to help it plan the tour, you should only respond in the JSON format described below based on the vistor information: 
{ 
    "Reasoning": "reasoning",
    "Tour": array of painting names,
    "TourID": array of painting ids from the data,
}

Here's an example: 
INPUT: I am a single man aged 34. I work as a Chinese art teacher at a university. Now my position is (0,0,0). Please help me plan a tour for this museum.
RESPONSE:
{
    "Reasoning": "The visitor has a special interest in Western art, show him more related paintings.",
    "Tour": ["Mona Lisa", "Last Supper", "Vitruvian Man", "The Scream", "Wheatfield with Crows", "Impression, Sunrise", "Guernica", "The Birth of Venus"]
    "TourID": ["painting 000", "painting 001","painting 002","painting 003","painting 004","painting 005","painting 006","painting 007"]
}

This museum has stored some paintings and  their names, spatial positions and orientations (both stored in Unity 3D coordinate format) are shown below:
Space: ```{data}```
"""