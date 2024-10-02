const inputBox = document.getElementById("inputter");
const formList = document.getElementById("form-list");
function addTask() {
    if (inputBox.value === '') {
        alert("Напиши хоть что-то!");
    } else {
        const task = {
            duty: inputBox.value,
            isCompleted: false
        };

        fetch('http://localhost:5026/api/TodoItems', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(task)
        })
            .then(response => response.json())
            .then(data => {
                createTaskElement(data);
            })
            .catch(error => console.error(error));

        inputBox.value = "";
    }
}

function createTaskElement(task) {
    let li = document.createElement("li");
    li.innerText = task.duty;
    li.setAttribute('data-id', task.id);
    if (task.isCompleted) {
        li.classList.add("completed");
    }
    formList.appendChild(li);

    let edit = document.createElement("img");
    edit.src = "204-2049954_banner-download-edit-draw-svg-png-icon-free.png";
    edit.alt = "редактировать";
    edit.classList.add("edit-btn");
    li.appendChild(edit);

    let span = document.createElement("span");
    span.innerHTML = "&#10060;";
    li.appendChild(span);
}

formList.addEventListener("click", function (event) {
    if (event.target.tagName === "SPAN") {
        const li = event.target.parentElement;
        const taskId = li.getAttribute('data-id');

        fetch(`http://localhost:5026/api/TodoItems/${taskId}`, {
            method: 'DELETE'
        })
            .then(() => {
                li.remove();
            })
            .catch(error => console.error(error));
    }
});

formList.addEventListener("click", function (event) {
    if (event.target.classList.contains("edit-btn")) {
        const li = event.target.parentElement;
        const taskId = li.getAttribute('data-id');
        const originalText = li.firstChild.nodeValue.trim();
        const newText = prompt('Отредактируйте текст:', originalText);

        if (newText !== null && newText.trim() !== '') {
            const updatedTask = {
                duty: newText
            };

            fetch(`http://localhost:5026/api/TodoItems/${taskId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(updatedTask)
            })
                .then(() => {
                    li.firstChild.nodeValue = newText;
                })
                .catch(error => console.error(error));
        }
    }
});

formList.addEventListener("click", function (event) {
    if (event.target.tagName === "LI") {
        const li = event.target;
        const taskId = li.getAttribute('data-id');
        const isCompleted = li.classList.contains("completed");

        const updatedTask = {
            isCompleted: !isCompleted
        };

        fetch(`http://localhost:5026/api/TodoItems/${taskId}/complete`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedTask)
        })
            .then(() => {
                li.classList.toggle("completed");
            })
            .catch(error => console.error(error));
    }
});

fetch('http://localhost:5026/api/TodoItems')
    .then(response => response.json())
    .then(data => {
        data.forEach(task => {
            createTaskElement(task);
        });
    })
    .catch(error => console.error(error));
