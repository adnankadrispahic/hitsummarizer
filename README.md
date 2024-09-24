# HitSummarizer Project

HitSummarizer is a full-stack web application that consists of a .NET Core Web API and a React Vite client app. This document will guide you through setting up and running both the API and the client app locally.

## Prerequisites

Make sure you have the following tools installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js (LTS)](https://nodejs.org/en/download/)

## Getting started
Start by cloning this repository

### 1. Running the .NET Core Web API
1. From root navigate to the server directory
    
    ```bash
    cd server
    ```
2. Restore dependencies
    
    ```bash
    dotnet restore
    ```
3. Navigate to API directory 

    ```bash
    cd HitSummarizer.API
    ```
4. Run .NET Core Web API

    ```bash
    dotnet run
    ```

### 2. Running the React Vite Client

Follow these steps:

1. From root navigate to `client` directory:
    ```bash
    cd client
    ```

2. Install the required dependencies using npm:
    ```bash
    npm install
    ```

3. Start the development server:
    ```bash
    npm run dev
    ```

4. The client will be available at [http://localhost:5173](http://localhost:5173) (or another port if configured differently).

**Note:** Make sure the Web API is running before starting the client, as the client will attempt to communicate with the API.