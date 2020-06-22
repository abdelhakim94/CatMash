function createImageElement(actualImageData, actualImageId, rivalImageData) {
  const imageElement = document.createElement("img");
  imageElement.src = actualImageData.url;
  imageElement.dataset.id = actualImageData.id;
  imageElement.id = actualImageId;
  imageElement.className = "photos";
  imageElement.alt = "Cute cat loading...";
  imageElement.onclick = function () {
    updateScores(
      "POST",
      `https://localhost:5001/api/score/${actualImageData.id}/${rivalImageData.id}`
    );
    loadPairImage("GET", "https://localhost:5001/api/pair");
  };
  return imageElement;
}

function updateImageElement(imageElement, dataId, src, clickEventHandler) {
  imageElement.src = src;
  imageElement.dataset.id = dataId;
  imageElement.onclick = clickEventHandler;
}

function insertPairImage(
  firstImage,
  firstImageParentId,
  secondImage,
  secondImageParentId
) {
  const firstImageParent = document.getElementById(firstImageParentId);
  const secondImageParent = document.getElementById(secondImageParentId);

  var existFirstImage = document.getElementById(firstImage.id);
  var existSecondImage = document.getElementById(secondImage.id);

  if (existFirstImage)
    updateImageElement(
      existFirstImage,
      firstImage.dataset.id,
      firstImage.src,
      function () {
        updateScores(
          "POST",
          `https://localhost:5001/api/score/${firstImage.dataset.id}/${secondImage.dataset.id}`
        );
        loadPairImage("GET", "https://localhost:5001/api/pair");
      }
    );
  else firstImageParent.appendChild(firstImage);

  if (existSecondImage)
    updateImageElement(
      existSecondImage,
      secondImage.dataset.id,
      secondImage.src,
      function () {
        updateScores(
          "POST",
          `https://localhost:5001/api/score/${secondImage.dataset.id}/${firstImage.dataset.id}`
        );
        loadPairImage("GET", "https://localhost:5001/api/pair");
      }
    );
  else secondImageParent.appendChild(secondImage);
}

function loadPairImage(method, link) {
  var request = new XMLHttpRequest();
  request.open(method, link, true);
  request.onload = function () {
    data = JSON.parse(this.response);
    if (request.status >= 200 && request.status < 400) {
      const firstImage = createImageElement(
        data.first,
        "firstImage",
        data.second
      );
      const secondImage = createImageElement(
        data.second,
        "secondImage",
        data.first
      );
      insertPairImage(
        firstImage,
        "firstImageCountainer",
        secondImage,
        "secondImageCountainer"
      );
    }
  };
  request.send();
}

function updateScores(method, link) {
  var request = new XMLHttpRequest();
  request.open(method, link, true);
  request.onload = function () {
    connection.invoke("UpdateVotesInClients");
  };
  request.send();
}
