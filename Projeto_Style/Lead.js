
window.onload = function() {
    const cursoSelect = document.getElementById('cursoInteresse');
    
    const cursos = [
        { id: 1, descricao: "Curso de Marketing" },
        { id: 2, descricao: "Curso de ADM" },
        { id: 3, descricao: "Curso de Design" }
    ];

    cursos.forEach(curso => {
        const option = document.createElement('option');
        option.value = curso.id;
        option.textContent = curso.descricao;
        cursoSelect.appendChild(option);
    });


    const form = document.getElementById('leadForm');
    form.addEventListener('submit', function(e) {
        e.preventDefault();

        const leadData = {
            nome: document.getElementById('nome').value,
            telefone: document.getElementById('telefone').value,
            email: document.getElementById('email').value,
            cursoInteresse: document.getElementById('cursoInteresse').value
        };

        console.log('Lead enviado:', leadData);
    });
};
