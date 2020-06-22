var connection = new signalR.HubConnectionBuilder().withUrl("/votes").build();

connection.on("UpdateVotes", function (totalVotes) {
  var totalVotesElement = document.getElementById("totalVotes");
  totalVotesElement.innerHTML = totalVotes + " votes 🔥";
});

connection.start();
