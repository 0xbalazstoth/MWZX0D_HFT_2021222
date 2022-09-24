fetch('http://localhost:43412/driver')
    .then(x => x.json())
    .then(y => console.log(y));