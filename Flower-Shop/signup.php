<?php
if ($_SERVER["REQUEST_METHOD"] === "POST") {
    $newUsername = $_POST["newUsername"];
    $newPassword = $_POST["newPassword"];
    
    // Simulate successful signup for demonstration purposes.
    // In a real application, you should add new users to a database.
    if ($newUsername !== "" && $newPassword !== "") {
        header("Location: index.html");
        exit;
    } else {
        header("Location: signup.html");
        exit;
    }
}
?>
