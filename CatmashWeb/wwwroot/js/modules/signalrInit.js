var connection = new signalR.HubConnectionBuilder().withUrl("/votes").build();

connection.on("UpdateVotes", function (totalVotes) {
  console.log("inside hub");
  var totalVotesElement = document.getElementById("totalVotes");
  totalVotesElement.innerHTML = totalVotes + " votes 🔥";
});

connection.start();
