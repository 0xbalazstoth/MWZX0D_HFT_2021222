let drivers = [];
let connection = null;
let driverIdToUpdate = -1;

getData();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:43412/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("DriverCreated", (user, message) =>
    {
        getData();
    });

    connection.on("DriverDeleted", (user, message) => {
        getData();
    });

    connection.on("DriverUpdated", (user, message) => {
        getData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getData() {
    await fetch('http://localhost:43412/driver')
        .then(x => x.json())
        .then(y => {
            drivers = y;
            display();
        });
}

function remove(id) {
    fetch('http://localhost:43412/driver/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((err) => { console.error('Error:', err); });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";

    drivers.forEach(x => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + x.driverId + "</td><td>"
            + x.name + "</td><td>" +
            x.number + "</td><td>" +
            x.nationality + "</td><td>" + 
            x.born + "</td><td>" +
            `<button type="button" onclick="remove(${x.driverId})">Delete</button>` +
            `<button style='margin-left: 5px' type="button" onclick="showupdate(${x.driverId})">Update</button>`
            + "</td></tr>";
    });
}

function dateToYMD(date) {
    var d = date.getDate();
    var m = date.getMonth() + 1;
    var y = date.getFullYear();
    return '' + y + '-' + (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d);
}

function showupdate(id) {
    document.getElementById('driverNameUpdate').value = drivers.find(x => x['driverId'] == id)['name'];
    document.getElementById('driverNumberUpdate').value = drivers.find(x => x['driverId'] == id)['number'];
    document.getElementById('driverNationalityUpdate').value = drivers.find(x => x['driverId'] == id)['nationality'];

    const born = drivers.find(x => x['driverId'] == id)['born'];
    var bornDate = new Date(born);
    const bornRes = dateToYMD(bornDate);

    document.getElementById('driverBornUpdate').value = bornRes;

    document.getElementById('updateformDiv').style.display = 'flex';

    driverIdToUpdate = id;
}

function create() {
    let name = document.getElementById('driverName').value;
    let number = document.getElementById('driverNumber').value;
    let nationality = document.getElementById('driverNationality').value;
    let born = document.getElementById('driverBorn').value;

    fetch('http://localhost:43412/driver', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Name: name, Number: number, Nationality: nationality, Born: born
            }),
        })
        .then(response => response)
        .then(data => {
            console.log(data);
            getData();
        })
        .catch(err => { console.error('Error:', err); });
}

function update() {
    document.getElementById('updateformDiv').style.display = 'none';

    let name = document.getElementById('driverNameUpdate').value;
    let number = document.getElementById('driverNumberUpdate').value;
    let nationality = document.getElementById('driverNationalityUpdate').value;
    let born = document.getElementById('driverBornUpdate').value;

    fetch('http://localhost:43412/driver', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                Name: name, Number: number, Nationality: nationality, Born: born, DriverId: driverIdToUpdate
            }),
    })
        .then(response => response)
        .then(data => {
            console.log(data);
            getData();
        })
        .catch(err => { console.error('Error:', err); });
}