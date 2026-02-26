# Contributing

## Project Status

This project is no longer actively maintained. However, you are welcome to clone the repository and make your own modifications. Below are the steps to guide you through this process.

### Cloning the Repository

1. **Clone the repository:**

   ```bash
   git clone https://github.com/namyus/koi-show-management-system-deploy
   ```

2. **Navigate into the project directory:**

   ```bash
   cd your-repo-name
   ```

### Branching Strategy (Recommended)

To manage your changes effectively, it's recommended to use a Git branching strategy. Here is a suggested strategy with three main branches:

- `main`: Contains the stable version of the code. Avoid direct commits to this branch.
- `dev`: Contains the latest development changes.
- `test`: Contains code that is under testing before being merged into `dev`.

### Working on a Feature

1. **Create a new branch from `dev` for your feature:**

   ```bash
   git checkout dev
   git pull origin dev
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes and commit them:**

   ```bash
   git add .
   git commit -m 'Add some feature'
   ```

3. **Push your branch to the repository:**

   ```bash
   git push origin feature/your-feature-name
   ```

### Testing and Merging

1. **Create a pull request (PR) from your feature branch to `test` for testing:**

   - Ensure all tests pass before requesting a merge.
   - Review and approve the PR.
   - Once approved, merge the PR into `test`.

2. **After thorough testing, create a pull request from `test` to `dev`.**

3. **For releases, create a pull request from `dev` to `main`.**

### Forking the Repository

If you prefer to fork the repository and manage your own copy:

1. **Go to the repository on GitHub.**

2. **Click on the `Fork` button at the top right of the page.**

3. **Clone your forked repository:**

   ```bash
   git clone https://github.com/namyus/koi-show-management-system-deploy
   ```

4. **Navigate into your project directory:**

   ```bash
   cd your-forked-repo-name
   ```

5. **Follow the branching strategy and development process as outlined above.**

By forking the repository, you can maintain and develop your own version independently.

## Running with Docker (Recommended for Contributors)

If you do not want to install SQL Server locally, you can run the full backend using Docker.

### Step 1: Install Docker Desktop

Download from:  
https://www.docker.com/products/docker-desktop

### Step 2: Start the Project

From the root project directory:

```bash
docker compose up --build
```
## This will start:

1. SQL Server container

2. API container

### Step 3: Access the API

1. API: http://localhost:8080

2. Swagger: http://localhost:8080/swagger

### Step 4: Stop Containers
   ```bash
   docker compose down
   ```
### Step 5: Reset Database (If Needed)
   ```bash
   docker compose down -v
   ```

 **This removes the SQL volume and recreates the database on the next startup.**
