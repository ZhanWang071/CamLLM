import openai
from museum import data
import json
import math

openai.api_key = "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"

def get_completion(request, messages, model="gpt-3.5-turbo"):
    prompt = request["question"]
    position = string_to_position(request["position"])

    messages.append({"role": "user", "content": prompt})
    
    response = openai.ChatCompletion.create(
        model=model,
        messages=messages,
        temperature=0, # this is the degree of randomness of the model's output
    )

    #  "{\n    \"Reasoning\": \"The visitor has a special interest in Chinese art, show him more related paintings.\",\n    \"Tour\": [\"Along the River During the Qingming Festival\", \"Dwelling in the Fuchun Mountains\", \"A Thousand Li of Rivers and Mountains\", \"Bamboo and Rock\", \"The Night Revels of Han Xizai\", \"Eighteen Songs of a Nomad Flute\", \"The Rongxi Studio\", \"The Red Cliff\"],\n    \"TourID\": [\"painting 008\", \"painting 009\", \"painting 010\", \"painting 011\", \"painting 012\", \"painting 013\", \"painting 014\", \"painting 015\"]\n}"
    result = response.choices[0].message["content"]
    result_ordered = reorder_landmarks(result, position)

    response.choices[0].message["content"] = result_ordered
    messages.append({"role": "assistant", "content": result_ordered})
    
    return response

def string_to_position(string_position):
    x, y, z = map(float, string_position[1:-2].split(','))
    return (x, y, z)

def distance(p1, p2):
    return math.sqrt((p1[0]-p2[0])**2 + (p1[2]-p2[2])**2)

def reorder_landmarks(result, start_position):
    
    tour_data = json.loads(result)
    tour_names = tour_data["Tour"]
    tour_ids = tour_data["TourID"]
    tour_positions = [data["paintings"][tour_id]["position"] for tour_id in tour_ids]

    num_landmarks = len(tour_positions)

    # Find the closest landmark to the start position
    closest_distance = float('inf')
    closest_index = -1
    for i in range(num_landmarks):
        dist = distance(start_position, tour_positions[i])
        if dist < closest_distance:
            closest_distance = dist
            closest_index = i
    
    # Start the greedy algorithm from the closest landmark to the start position
    current_index = closest_index
    visited = [False] * num_landmarks

    # Reorder the Tour and TourID lists with the greedy algorithm
    reordered_tour = []
    reordered_tour_id = []

    for _ in range(num_landmarks):
        current_landmark = tour_names[current_index]
        current_landmark_id = tour_ids[current_index]
        reordered_tour.append(current_landmark)
        reordered_tour_id.append(current_landmark_id)
        visited[current_index] = True

        # Find the closest unvisited landmark to the current landmark
        closest_distance = float('inf')
        closest_index = -1
        for i in range(num_landmarks):
            if not visited[i]:
                dist = distance(tour_positions[current_index], tour_positions[i])
                if dist < closest_distance:
                    closest_distance = dist
                    closest_index = i

        # Move to the closest unvisited landmark
        current_index = closest_index

    tour_data["Tour"] = reordered_tour
    tour_data["TourID"] = reordered_tour_id

    return json.dumps(tour_data, indent=4)

if __name__ == "__main__":
    result = "{\n    \"Reasoning\": \"The visitor has a special interest in Chinese art, show him more related paintings.\",\n    \"Tour\": [\n        \"Guernica\",\n        \"The Birth of Venus\",\n        \"The Scream\",\n        \"The Great Wave off Kanagawa\",\n        \"The Persistence of Memory\",\n        \"The Last Judgment\",\n        \"The Creation of Adam\",\n        \"The Starry Night\"\n    ],\n    \"TourID\": [\n        \"painting 015\",\n        \"painting 013\",\n        \"painting 014\",\n        \"painting 008\",\n        \"painting 010\",\n        \"painting 012\",\n        \"painting 011\",\n        \"painting 009\"\n    ]\n}"
    start_position = (0,0,0)
    result_ordered = reorder_landmarks(result, start_position)
    print(result_ordered)