# Bank Account

Welcome to the Bank Account project! This simple demo allows users to create an account, deposit and withdraw funds, and view their transaction histories. The project is built using .NET 8, employs JWT bearer tokens and refresh tokens stored in HTTP-only cookies for authorization, and utilizes Entity Framework with a code-first approach, requiring migration.

## Getting Started

To run the project locally, follow these steps:

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Postman](https://www.postman.com/) for API testing

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/SqueezedLemon/Bank.git
    cd bank
    ```

2. Run Entity Framework migration:

    ```bash
    dotnet ef database update
    ```

3. Start the application:

    ```bash
    dotnet run
    ```

The application should now be running locally.

## Authorization

The project uses JWT bearer tokens and refresh tokens, stored in HTTP-only cookies for secure authorization. 

## API documentation

For API documentation, refer to the [Postman Documentation](https://documenter.getpostman.com/view/16124553/2s9YkrcLBX).

## Contributing

Currently, there is no contribution guideline available. Feel free to contribute by forking the repository and submitting a pull request.

## License

This project is not licensed at the moment. You may use it as per your requirements.

## Acknowledgments

Thank you for checking out this Bank Account project! If you have any questions or concerns, feel free to open an issue.

Happy coding! 🚀
