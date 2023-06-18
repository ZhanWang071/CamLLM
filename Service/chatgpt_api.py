import openai

openai.api_key = "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"

def get_completion(prompt, messages, model="gpt-3.5-turbo"):
    messages.append({"role": "user", "content": prompt})
    
    response = openai.ChatCompletion.create(
        model=model,
        messages=messages,
        temperature=0, # this is the degree of randomness of the model's output
    )

    result = response.choices[0].message["content"]
    messages.append({"role": "assistant", "content": result})
    # print(messages)
    return response