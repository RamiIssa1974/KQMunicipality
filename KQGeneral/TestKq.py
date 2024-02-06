import concurrent.futures
import requests
 
request_counter = 0

def make_request(url): 
    global request_counter
    request_counter += 1
    
    response = requests.get(url)
    print(f"{request_counter}-Response from {url}: {response.status_code}")

# List of URLs to simulate concurrent requests
#urls = ["https://reg.kfar-qasem.muni.il/Registration/Start"] * 3000
urls = ["https://reg.kfar-qasem.muni.il/Registration/Reject/?studentIDN=123456789&studentBirthDate=2019-01-30&year=2024&Registration_Type=1"] * 20
# Use ThreadPoolExecutor for concurrent requests
 
with concurrent.futures.ThreadPoolExecutor() as executor:
    print("before executing")
    executor.map(make_request, urls)
    print("after executing")