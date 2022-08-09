let getAllBtn = document.getElementById("btn1");
let getByIdBtn = document.getElementById("btn2");
let addNewUser = document.getElementById("btn3");

let getByIdInput = document.getElementById("input1");
let addUserByInput = document.getElementById("input2");

let port = "5037";

let getAllUsers = async()=>{
    let url = "http://localhost:" + port + "/api/users";

    let response = await fetch(url);
    let data = await response.json();
    console.log(data);
};

let getUserById = async () => {
    let url = "http://localhost:" + port + "/api/users/" + getByIdInput.value;
    let response = await fetch(url);
    let data = await response.text();
    console.log(data);
};

let addUser = async () => {
    let url = "http://localhost:" + port + "/api/users";
    let response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'text/plain'
        },
        body: addUserByInput.value 
   });
    let data = await response.text();
    console.log(data);
}


getAllBtn.addEventListener("click", getAllUsers);
getByIdBtn.addEventListener("click", getUserById);
addNewUser.addEventListener("click", addUser);