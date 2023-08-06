import openai
from museum import data
import json
import math
import museum
import prompts
import copy

openai.api_key = "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"

# messages = [{"role": "system", "content": "You are a helpful assistant."}]
messages = [{"role": "system", "content": prompts.init_prompt}]

def gpt_guidance(request):
    question = request["question"]
    position = string_to_position(request["position"])
    if (len(request["landmark"])):
        landmark = data["paintings"][request["landmark"]]["name"]
    else:
        landmark = request["landmark"]

    messages.append({"role": "user", "content": question})

    print(messages)

    tasks = task_classify(question)
    print("Task Classification: " + str(tasks))

    if "information enhancement" in tasks:
        response = gpt_information(question, position, landmark)
    if "preference specification" in tasks:
        response = gpt_preference(question)
    if "navigation" in tasks:
        response = gpt_navigation(question, position, landmark)
    
    messages.append({"role": "assistant", "content": response})
    print(messages)

    return [tasks, response]

"""Step 1: classify the user question into which kinds of interaction tasks
"""
def task_classify(request, model="gpt-3.5-turbo"):
    # request = prompts.task_classify_prompt+ "INPUT:\n" + request +"\nRESPONSE:\n"
    task_classify_messages = [{"role": "system", "content": prompts.task_classify_prompt}]
    task_classify_messages.append({"role": "user", "content": request})

    response = openai.ChatCompletion.create(
        model=model,
        messages=task_classify_messages,
        temperature=0,
    )
    result = response.choices[0].message["content"]     # Preference Specification, Navigation

    # Remove parentheses if they exist
    if result.startswith("(") and result.endswith(")"):
        result = result[1:-1]

    result = result.split(', ')     # ['Preference Specification', 'Navigation']
    return result


"""
Step 2: According classification results, assign the task into the corresponsding models and get the response
    - navigation: 1) a tour; 2) direct search for one item
    - information enhancement: give related information
    - preference specification: 
"""

# TODO: We need to differentiate the tour and direct search
def gpt_navigation(question, position, landmark,model="gpt-3.5-turbo"):
    print("Navigation: ")

    # remember the history conversation and give the response
    navigation_messages = copy.deepcopy(messages)
    navigation_messages[0]["content"] = museum.navigation_prompt
    print(navigation_messages)
    response = openai.ChatCompletion.create(
        model=model,
        messages=navigation_messages,
        temperature=0,
    )
    result = response.choices[0].message["content"] 
    print(result)
    result_ordered = reorder_landmarks(result, position)

    return result_ordered

def gpt_information(question, position, landmark, model="gpt-3.5-turbo"):
    print("Information Enhancement: ")
    if (len(landmark)):
        messages[-1]["content"] = "Now I am looking at the painting " + str(landmark) + ". " + question
    else:
        messages[-1]["content"] = "Now I am standing at the position in the model: " + str(position) + ". " + question

    response = openai.ChatCompletion.create(
        model=model,
        messages=messages,
        temperature=0,
    )

    result = response.choices[0].message["content"]
    print(result)
    return result

# TODO: Analysis user preference and give some natural response
def gpt_preference(question, model="gpt-3.5-turbo"):
    print("User Preference: ")

    response = openai.ChatCompletion.create(
        model=model,
        messages=messages,
        temperature=0,
    )
    result = response.choices[0].message["content"]
    print(result)
    return result

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