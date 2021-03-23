import game_analytics
import requests

def test_post_data():
   url = 'http://localhost:5000/upload_data'
   payload = {"completion_time": 2000,
              "level": 7
               }
   r = requests.post(url, json=payload)
   print(r) 

def test_get_data():
   game_analytics.connect_to_database_outside_docker()
   #game_analytics.get_all_data()
   df = game_analytics.get_dataframe()
   print(df)

test_post_data()
test_get_data()
