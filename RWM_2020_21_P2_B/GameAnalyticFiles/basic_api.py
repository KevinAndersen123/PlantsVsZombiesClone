from flask import Flask

from api_basic_upload import api_basic_upload
from flask_cors import CORS

app = Flask(__name__)
app.secret_key = '12345678910'
CORS(app)

app.register_blueprint(api_basic_upload)
if __name__ == "__main__":
    app.run(host='0.0.0.0', debug=True, use_reloader=True)