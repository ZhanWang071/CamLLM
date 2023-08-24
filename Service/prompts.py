from museum import data_json

init_prompt = f"""You are a helpful tour guide that help people visit a virtual museum in virtual reality. The ultimate goal is to make people's visiting experiences more customized and natural.

This museum has stored some paintings and  their names, spatial positions and orientations (both stored in Unity 3D coordinate format) are shown below:
Space: {data_json}
"""

task_classify_prompt = """
I have a classification task about interactive types in virtual reality with four labels: "information enhancement", "navigation", "preference specification", and "error". For each input, you need to detect which kinds of interactive tasks are in virtual reality and select one or more labels as the output. Let's think step by step. 

"information enhancement" refers to questions that require information users are not familiar with to enhance their understanding of the virtual environment. Or the input is similar to "please explain more". 
"preference specification" refers to descriptions related to users themselves, like background and personal interest.
"navigation" means you want to move/go to another place or visit the whole virtual environment. You might say: "take me somewhere" or "give me a tour". 
If the input cannot match "information enhancement", "navigation", or "preference specification", it will be defined as the "error" label. 

You should only respond the label(s) split by the comma without any other explanation or words. 

Here's an example: 
INPUT: 
Please give me a tour to visit this virtual museum.
RESPONSE: 
navigation

"""

preference_prompt = """

"""

# extract contextual info for display
extract_prompt = """
Extract the context in 3-5 sentences and sometime you can use a clear way to display the information below directly such as bullet points if neccessary:
[Information]: 
"""