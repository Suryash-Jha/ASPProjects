import requests
import json
url = 'https://aquamediumorchidvoxels.suryashjha.repl.co/tasks'
data = {
    "free": "bry",
    "link": "hrttps://hyfdgh",
    "date-added": "2020-10-10"
}

req= json.dumps(data)
response = requests.post(url, json=req)

print(response.status_code)
print(response.text)
