function LoadPlayerColors() {
    const borderClass = "border";
    const tables = document.getElementsByTagName("table");
    const containers = document.getElementsByClassName("container");

    for (let x = 0; x < tables.length; x++) {
        let table = tables[x];
        const playerKey = table.dataset.playerNumber;
        switch (playerKey) {
            case "1": tableColor = "primary"; squareColor = "dodgerblue"; break;
            case "2": tableColor = "success"; squareColor = "green"; break;
            case "3": tableColor = "warning"; squareColor = "yellow"; break;
            case "4": tableColor = "danger"; squareColor = "red"; break;
            case "5": tableColor = "secondary"; squareColor = "grey"; break;
            default: tableColor = "info"; squareColor = "blue"; break;
        }
        table.classList.add("table-" + tableColor);
        table.classList.add(borderClass);
        table.classList.add(borderClass + "-" + tableColor);

        containers[x].classList.add(borderClass + "-" + tableColor);

        const emptySquares = table.querySelectorAll(".empty-square");
        for (let i = 0; i < 12; i++) {
            emptySquares[i].setAttribute("style", "background-color: " + squareColor);
        }

        const allRows = table.querySelectorAll(".row");
        for (let i = 0; i < allRows.length; i++) {
            allRows[i].classList.add(borderClass);
            allRows[i].classList.add(borderClass + "-" + tableColor);
        }
    }
};

function ConstructBsTable() {
    const tables = document.getElementsByTagName("table");

    for (let x = 0; x < tables.length; x++) {
        let table = tables[x];
        const rows = table.getElementsByTagName("tr");

        for (let r = 0; r < rows.length; r++)
        {
            const cells = rows[r].getElementsByTagName("td");
            cells[0].setAttribute("scope", "row");
        }
    }
}

window.onload = function () {
    LoadPlayerColors();
    ConstructBsTable();
};