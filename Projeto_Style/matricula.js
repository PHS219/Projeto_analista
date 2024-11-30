window.onload = function() {
    const cursoSelect = document.getElementById('curso');
    const turmaSelect = document.getElementById('turma');

    fetch('http://localhost:5000/api/matricula/turmas/1')  
        .then(response => response.json())
        .then(cursos => {
            cursos.forEach(curso => {
                const option = document.createElement('option');
                option.value = curso.Id;
                option.textContent = curso.Descricao;
                cursoSelect.appendChild(option);
            });
        });

    cursoSelect.addEventListener('change', function() {
        const cursoId = this.value;
        turmaSelect.disabled = false;

        fetch(`http://localhost:5000/api/matricula/turmas/${cursoId}`)
            .then(response => response.json())
            .then(turmas => {
                turmaSelect.innerHTML = '';  
                turmas.forEach(turma => {
                    const option = document.createElement('option');
                    option.value = turma.Id;
                    option.textContent = turma.Descricao;
                    turmaSelect.appendChild(option);
                });
            });
    });

    const form = document.getElementById('matriculaForm');
    form.addEventListener('submit', function(e) {
        e.preventDefault();

        const matriculaData = {
            nome: document.getElementById('nome').value,
            email: document.getElementById('email').value,
            telefone: document.getElementById('telefone').value,
            cursoId: cursoSelect.value,
            turmaId: turmaSelect.value
        };

        fetch('http://localhost:5000/api/matricula/matricular', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(matriculaData)
        })
        .then(response => response.json())
        .then(data => {
            alert(data);  
        });
    });
};
