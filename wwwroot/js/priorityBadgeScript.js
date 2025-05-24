document.addEventListener("DOMContentLoaded", function () {
  console.log("Priority badge script loaded!");

  const prioritySelect = document.getElementById("prioritySelect");
  const priorityBadge = document.getElementById("priorityBadge");

  console.log("Select:", prioritySelect);
  console.log("Badge:", priorityBadge);

  const priorityColors = {
    2: "red",
    1: "orange",
    0: "green",
  };

  function updateBadgeColor() {
    if (!prioritySelect || !priorityBadge) return;
    const selectedValue = prioritySelect.value;
    const color = priorityColors[selectedValue] || "gray";
    priorityBadge.style.backgroundColor = color;
  }

  if (prioritySelect) {
    updateBadgeColor();
    prioritySelect.addEventListener("change", updateBadgeColor);
  }
});
