# DronesAPI


This repository contains a minimal API implemented in C# that simulates a drone delivery service. The API allows clients to communicate with the drones and perform various operations such as registering a drone, loading it with medication items, checking loaded medication items for a given drone, checking available drones for loading, and checking the battery level of a drone.

## Getting Started

To get started with the API, follow the instructions below.

### Prerequisites
.NET 6.0 SDK or later

### Installation

1. Clone this repository to your local machine.

```bash
git clone https://github.com/deathekid100/DroneAPI.git
```

2. Navigate to the project directory.

```bash
cd DroneAPI
```

3. Build the project.


```bash
dotnet build
```

### Running the API
```bash
dotnet run --project DronesAPI
```

## Usage

The API provides the following endpoints:

    POST /api/v1/drones: Registers a new drone with the specified details.
    POST /api/v1/drones/{id}/load: Loads medication items onto the specified drone.
    GET /api/v1/drones/{id}/medications: Retrieves the loaded medication items for the specified drone.
    GET /api/v1/drones/available: Retrieves the list of available drones for loading.
    GET /api/v1/drones/{id}/batterylevel: Retrieves the battery level of the specified drone.

Make sure to include the required JSON payloads and parameters when making requests to the API.

## Testing
To run the tests, execute the following command from the project directory:

```bash
dotnet test
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
