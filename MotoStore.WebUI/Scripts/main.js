fetch('/getUniqCategories')
  .then(function(response) {
    return response.json();
   })
  .then(function(res) {
    console.log(res);
  })