//goes into the html
/*var services;

function checkLogin() {
  $.get("?tableName=customers", function(customerTable){
    console.log(customerTable);
    customers.JSON.parse(customerTable);
      /*services = JSON.parse(orderTable);
      services.forEach(function(order){
    
    $("#prd").append("<tr><td width=200px>" + service.name + "</td> <td width=50px>Price</td>" + service.description + "<td width=170px><input type='number' id= " + service.price + "></td></tr>");
    
    });
      $("#products").append("<div id='totalCost'> </div>")
    
    }); // end $.get
}*/
    
function getServices(){

$.get("?tableName=orders", function(orderTable){
console.log(orderTable);
    $("#servicesGrid").empty();
  /*services = JSON.parse(orderTable);
  services.forEach(function(order){

$("#prd").append("<tr><td width=200px>" + service.name + "</td> <td width=50px>Price</td>" + service.description + "<td width=170px><input type='number' id= " + service.price + "></td></tr>");

});
  $("#products").append("<div id='totalCost'> </div>")*/

}); // end $.get

} // getProducts()

{/* function calculateCost(){ */}

    {/* var sum=0;
    if (services) {
        $('#totalcost').empty();
        services.forEach(function(product){
            sum = sum + service.price * $("#" + service.name).val();
        });
        sum = sum * 1.1; // 10% tax
        $("#totalCost").append("<div> Cost = $ " + sum + " incl of 10% of 10% tax </div>"); */}

{/* } //calculateCost() */}