fetch('/getUniqCategories')
  .then(function(response) {
    return response.json();
   })
  .then(function(res) {
    console.log(res);
    })
//fetch("/addNewUser",
//    {
//        headers: {
//            'Content-Type': 'application/json'
//        },
//        method: "POST",
//        body: JSON.stringify({ Id: "1", password: "2",username:"snilk" })
//    })
//    .then(function (res) { console.log(res) })
//    .catch(function (res) { console.log(res) })
//$.ajax({
//    type: "GET",
//    url: "/addNewUser",
//    contentType: "application/json; charset=utf-8",
//    dataType: "json",
//    data: JSON.stringify({name:"John",password:"123"}),    
//});
$.post("/addNewUser", { username: "John", password: "2" })
    .done(function (data) {
        console.log("Data recieved: " + data);
    });