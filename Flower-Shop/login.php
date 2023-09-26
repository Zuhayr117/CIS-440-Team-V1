<?php
if ($_SERVER["REQUEST_METHOD"] === "POST") {
    $username = $_POST["username"];
    $password = $_POST["password"];
    
    // Admin credentials
    $adminUsername = "admin";
    $adminPassword = "adminpassword";

    // Perform authentication logic here. Replace with your actual authentication code.
    if ($username === "your_username" && $password === "your_password") {
        // Regular user login, redirect to main.html
        header("Location: index.html");
        exit;
    } elseif ($username === $adminUsername && $password === $adminPassword) {
        // Admin login, redirect to admin.html
        header("Location: admin.html");
        exit;
    } else {
        // Authentication failed, redirect back to login page
        header("Location: log in.html");
        exit;
    }
}

?>
