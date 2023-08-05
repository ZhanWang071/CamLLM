# from chatgpt_wrapper import ChatGPT
from flask import Flask, request, jsonify
from chatgpt_api import gpt_guidance
import museum

app = Flask(__name__)

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
        print("ChatGPT Question is: {}".format(question))
    
    response = gpt_guidance(prompt)

    if args.get("debug", default=False, type=bool):
        print("ChatGPT Response Received...")
        print(response)
    
    return jsonify(content=response)

if __name__ == "__main__":
    app.run(threaded=False)