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
If the input is not related to the current items of this environment, it will be defined as the "error" label. 

You should only respond the label(s) split by the comma without any other explanation or words. 

Here are examples: 
INPUT: 
Please give me a tour to visit this virtual museum.
RESPONSE: 
navigation

INPUT: 
Please guide me to the three most popular items.
RESPONSE: 
navigation, information enhancement

INPUT: 
Take me to visit the three most popular items.
RESPONSE: 
navigation, information enhancement

INPUT:
I am a chinese art teacher.
RESPONSE:
preference specification

INPUT:
Next
RESPONSE:
navigation

INPUT:
I want to see some modern style paintings, could you?
RESPONSE:
navigation, information enhancement, preference specification

INPUT:
What is the result for one plus one.
RESPONSE:
error

INPUT:
"""

preference_prompt = """

"""

# extract contextual info for display
extract_prompt = """
1. detect if the user start visiting, during the tour, or finishing the tour.
2. If the user is at the beginning or ending of the tour, extract the context in 2-3 sentences and sometime you can use a clear way to display the information below directly such as bullet points if neccessary.
3. Otherwise, extract the important points with words or one-sentence description.
4. ONLY give the RESPONSE.

Here are several examples:
INPUT:
The Great Wave off Kanagawa is a woodblock print by Japanese ukiyo-e artist Hokusai, created in late 1831 during the Edo period of Japanese history. The print depicts three boats moving through a storm-tossed sea, with a large wave forming a spiral in the centre and Mount Fuji visible in the background.
RESPONSE:
Name: The Great Wave off Kanagawa
Author: Hokusai
Year: 1831
Country: Japan
Introduction: The print depicts three boats moving through a storm-tossed sea, with a large wave forming a spiral in the centre and Mount Fuji visible in the background.

INPUT: 
"""

info_prompt = """
You need to give the answers following the steps:
1. detect if the user start visiting, during the tour, or finishing the tour.
2. If the user is at the beginning or ending of the tour, give the general description and suggestions for overview or summarization respectively. Response in less than 5 sentences. 
3. During the progress, if the user is only interested in the current painting,  only give the most imporatant details about this painting like author, year, and style. Response in less than 3 sentences. 
4. During the progress, if the user relate to or compare different paintings. List all the related paintings name and one-sentence bio in a direct and clear way.
4. Do not mention anything in a mathmatical way.

Here are some examples:
INPUT:
Now I am looking at the painting "The Great Wave". Please introduce me this painting.
Reason: The user is during the tour, and want to get an introcution about the painting "The Great Wave".
RESPONSE:
The Great Wave off Kanagawa is a woodblock print by Japanese ukiyo-e artist Hokusai, created in late 1831 during the Edo period of Japanese history. The print depicts three boats moving through a storm-tossed sea, with a large wave forming a spiral in the centre and Mount Fuji visible in the background.

INPUT:
Now I am looking at the painting "The Greate Wave". Is there any other painting of the same style in this museum.
Reason: The user is during the tour, and want to get related paintings about the painting "The Great Wave" belonging to the same painting style.
RESPONSE:
Yes, the painting style of "The Great Wave" is [style name]. Annd there are some other paintings of the same style as "The Great Wave":
- "Li River in a Splashed-Ink Landscape": a Chinese painting that captures the serene beauty of the Li River through the unique technique of splashed-ink, depicting the ethereal landscapes in an evocative and abstract manner.

"""