import json
from flask_cors import CORS
from flask import Flask, render_template, request
import search

app = Flask(__name__, static_folder='static')
CORS(app)

@app.route('/search/<search_string>')
def search_(search_string):
    return json.dumps(search.search(search_string))

@app.route('/<path:target>')
def resource(target):
    return app.send_static_file(target)

if __name__ == '__main__':
    app.run(port=80, host='0.0.0.0')