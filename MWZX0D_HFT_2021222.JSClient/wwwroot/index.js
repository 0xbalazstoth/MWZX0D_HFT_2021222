let drivers = [];

fetch('http://localhost:43412/driver')
    .then(x => x.json())
    .then(y => {
        drivers = y;
        console.log(drivers);

        display();
    });

function display() {
    drivers.forEach(x => {
        var table = document.getElementById('resultarea');
        const tr = document.createElement('tr');

        const idTd = document.createElement('td');
        const nameTd = document.createElement('td');
        const numberTd = document.createElement('td');
        const nationalityTd = document.createElement('td');
        const bornTd = document.createElement('td');
        const teamIdTd = document.createElement('td');

        const driverId = document.createTextNode(x.driverId);
        const driverName = document.createTextNode(x.name);
        const driverNumber = document.createTextNode(x.number);
        const driverNationality = document.createTextNode(x.nationality);
        const driverBorn = document.createTextNode(x.born);
        const driverTeamId = document.createTextNode(x.teamId);

        idTd.appendChild(driverId);
        nameTd.appendChild(driverName);
        numberTd.appendChild(driverNumber);
        nationalityTd.appendChild(driverNationality);
        bornTd.appendChild(driverBorn);
        teamIdTd.appendChild(driverTeamId);

        tr.appendChild(idTd);
        tr.appendChild(nameTd);
        tr.appendChild(numberTd);
        tr.appendChild(nationalityTd);
        tr.appendChild(bornTd);
        tr.appendChild(teamIdTd);

        table.appendChild(tr);
    });
}