# CafeHub Solution

This project is a comprehensive solution for a Cafe Management System, utilizing a range of technologies including .NET 6, React JS, and Docker. This README provides instructions for running the entire solution using Docker Compose.

## Prerequisites

Before running the Docker Compose setup, ensure you have the following installed:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Project Structure

The solution includes the following projects:

- `CafeHub.Api` - The API layer of the application.
- `CafeHub.Core` - Core business logic and entities.
- `CafeHub.Application` - Application services and logic.
- `CafeHub.Infrastructure` - Data access and infrastructure components.
- `CafeHub.UI` - The React frontend for the application.


### Configuration

1. **CafeHub.Api/Dockerfile**: Contains configuration for building the .NET 6 API container.
2. **CafeHub.UI/Dockerfile**: Contains configuration for building the React JS frontend container.
3. **docker-compose.yml**: Defines the services and how they interact.

### Build and Run

1. Open a terminal and navigate to the root directory of the project.

2. Run the following command to build and start the containers:

    ```bash
    docker-compose up --build
    ```

    This command will build the images and start the containers defined in `docker-compose.yml`.

3. To stop the running containers, use:

    ```bash
    docker-compose down
    ```

### Access the Application

- **Frontend (React JS)**: Open your browser and navigate to `http://localhost:3000`.
- **Backend (API)**: The API will be available at `http://localhost:9000/swagger`.

## Environment Variables

Configure any required environment variables in a `.env` file in the root directory. Docker Compose will automatically use these variables.

## Troubleshooting

- **Port Conflicts**: Ensure that the ports specified in `docker-compose.yml` are not already in use by other services.
- **Build Issues**: If you encounter build issues, try running `docker-compose build` separately to get more detailed error messages.

## Contributing

Feel free to open issues or submit pull requests. Ensure that your code follows the existing coding style and includes appropriate tests.

## License

This project is created as a demonstration purpose of CafeHub end to end implementation.



