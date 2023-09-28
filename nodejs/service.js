var express = require('express');
var app = express();
var bodyParser = require('body-parser');
var mysql = require("mysql");

// Configure body-parser middleware to parse JSON data
app.use(bodyParser.json());

// Your MySQL configuration
var con = mysql.createConnection({
    host: "107.180.1.16",
    user: "fall2023team4",
    password: "fall2023team4",
    database: "fall2023team4"
});
con.connect();

// Define a route to handle the GET request for fetching orders
app.get('/getOrders', function (req, res) {
    var myQuery = "SELECT * FROM Orders;";
    
    con.query(myQuery, function(err, result, fields) {
        if (err) {
            console.error("Error fetching orders: " + err);
            res.status(500).send("Error fetching orders");
        } else {
            console.log("Fetched orders from the database");
            res.status(200).json(result);
        }
    });
});

// Define a route to handle the GET request for fetching orders
app.get('/getProducts', function (req, res) {
    var myQuery = "SELECT * FROM Products;";
    
    con.query(myQuery, function(err, result, fields) {
        if (err) {
            console.error("Error fetching products: " + err);
            res.status(500).send("Error fetching orders");
        } else {
            console.log("Fetched products from the database");
            res.status(200).json(result);
        }
    });
});

// Define a route to handle the GET request for fetching orders
app.get('/getMostRecentOrder', function (req, res) {
    var myQuery = "SELECT order_id FROM Orders ORDER BY order_id DESC LIMIT 1;";
    
    con.query(myQuery, function(err, result, fields) {
        if (err) {
            console.error("Error fetching products: " + err);
            res.status(500).send("Error fetching orders");
        } else {
            console.log("Fetched products from the database");
            res.status(200).json(result);
        }
    });
});

// Define a route to handle the GET request for fetching customers
app.get('/getCustomers', function (req, res) {
    var myQuery = "SELECT * FROM Customers;";
    
    con.query(myQuery, function(err, result, fields) {
        if (err) {
            console.error("Error fetching customers: " + err);
            res.status(500).send("Error fetching customers");
        } else {
            console.log("Fetched customers from the database");
            res.status(200).json(result);
        }
    });
});

// Define a route to handle the POST request for adding a new user
app.post('/insertCustomer', function (req, res) {
    console.log("inside insertCustomers");
    var newName = req.body.customer_name;
    var newUsername = req.body.customer_username;
    var newPassword = req.body.customer_password;

    // Define your SQL query to insert data
    var myQuery = "INSERT INTO Customers (customer_name, customer_username, customer_password) VALUES (?, ?, ?)";
    
    con.query(myQuery, [newName, newUsername, newPassword], function(err, result){
        if (err) {
            console.error("Error adding a new user: " + err);
            res.status(500).send("Error adding a new user");
        } else {
            console.log("New user added to the database");
            res.status(200).send("New user added to the database");
        }
    });
});

// Define a route to handle the POST request for adding a new user
app.post('/insertOrder', function (req, res) {
    console.log("inside insertOrder");
    /*var customerId = req.body.customer_id;
    var orderId = req.body.order_id;
    var productId = req.body.product_id;
    var orderAmount = req.body.order_amount;*/

    var customerId = 2;
    var orderId = 3;
    var productId = 3;
    var orderAmount = 10;
    
    // Define your SQL query to insert data
    var myQuery = "INSERT INTO Orders (customer_id, order_id, product_id, order_amount) VALUES (?, ?, ?, ?)";
    con.query(myQuery, [customerId, orderId, productId, orderAmount], function(err, result){
        if (err) {
            console.error("Error adding a new order: " + err);
            res.status(500).send("Error adding a new order");
        } else {
            console.log("New order added to the database");
            res.status(200).send("New order added to the database");
        }
    });
});

// Serve static files (HTML, CSS, JavaScript)
app.use(express.static(__dirname));

app.listen(8000, function() {
    console.log("\nThe Web server is alive!!!\n" + "It's listening on http://127.0.0.1:8000 or http://localhost:8000");
});