# from chatgpt_wrapper import ChatGPT
from flask import Flask, request, jsonify
from chatgpt_api import get_completion
import museum

app = Flask(__name__)
# messages = [{"role": "system", "content": "You are a helpful assistant."}]
messages = [{"role": "system", "content": museum.startPrompt}]


@app.route("/chatgpt/status", methods=["GET"])
def status():
    return jsonify(status="ok")

@app.route("/chatgpt/question", methods=["POST"])
def question():
    args = request.args
    prompt = request.json
    question = prompt["question"]

    if args.get("debug", default=False, type=bool):
        print("ChatGPT Question Received...")
        print("ChatGPTR Question is: {}".format(question))
    
    response = get_completion(prompt, messages)

    if args.get("debug", default=False, type=bool):
        print("ChatGPT Response Received...")
        print(response)
    
    return response

if __name__ == "__main__":
    app.run(threaded=False)