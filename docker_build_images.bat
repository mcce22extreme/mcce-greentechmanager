docker build -t domih/greentechmanager.apigateway -f ".\src\GreenTechManager.ApiGateway\Dockerfile" .

docker build -t domih/greentechmanager.identity -f ".\src\GreenTechManager.Identity\Dockerfile" .

docker build -t domih/greentechmanager.operators -f ".\src\GreenTechManager.Operators\Dockerfile" .

docker build -t domih/greentechmanager.solarparks -f ".\src\GreenTechManager.SolarParks\Dockerfile" .

docker build -t domih/greentechmanager.windparks -f ".\src\GreenTechManager.WindParks\Dockerfile" .