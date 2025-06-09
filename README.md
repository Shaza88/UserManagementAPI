# UserManagementAPI

UserManagementAPI is a simple RESTful API that provides JWT-based authentication and user management functionality. This guide explains how to use the API effectively.

---

## Authentication and Authorization

Before you can access any secured endpoints, you must authenticate and obtain a JWT token.

### Step 1: Authenticate

**Endpoint:**

```
POST /auth/login
```

**Request Body Example:**

```json
{
  "username": "admin",
  "password": "password"
}
```

**Response Example:**

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6..."
}
```

This token is required to authorize further requests.

---

### Step 2: Authorize Using Swagger UI

1. Open the Swagger UI (typically found at `/swagger`).
2. Click the **Authorize** button (🔒) at the top right corner.
3. In the input field, enter the token in this format:

```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...
```

4. Click **Authorize** and then **Close** the popup.

Once authorized, you can interact with all secured endpoints directly in Swagger.

---

## Making Requests

After authorizing, you can use the secured endpoints. Swagger will automatically include the token in the `Authorization` header of each request.

You can also make requests using tools like Postman or curl by setting the header manually:

**Example Header:**

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...
```

---

## Workflow Summary

1. Call `POST /auth/login` with valid credentials.
2. Copy the returned JWT token.
3. Paste the token into Swagger’s Authorize modal using the `Bearer` format.
4. Access and test all available endpoints.

---

## Notes

- All secured endpoints require a valid JWT token in the `Authorization` header.
- If a request returns `401 Unauthorized`, make sure:
  - You have logged in successfully
  - The token is not expired
  - The token was provided in the correct format: `Bearer <token>`

---

## Support

If you have questions or issues, please open an issue in this repository.
