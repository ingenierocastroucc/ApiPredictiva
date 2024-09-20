document.getElementById('update-button').addEventListener('click', () => {
    // URL de la API
    const apiUrl = 'https://localhost:7170/api/Employees';

    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {

            console.log(data); 

            if (!Array.isArray(data) || data.length === 0) {
                console.error('Los datos no tienen la estructura esperada.');
                return;
            }

            // Extrae los datos necesarios
            const values = data.map(employee => {
                if (employee && employee.empId !== undefined && employee.firstName) {
                    return {
                        id: employee.empId,
                        label: `${employee.firstName}`, // Solo el primer nombre
                        value: 1 // Valor fijo para el eje Y
                    };
                } else {
                    console.error('Datos del empleado no válidos:', employee);
                    return null; 
                }
            }).filter(d => d !== null); 

            if (values.length === 0) {
                console.error('No se pudieron procesar los datos.');
                return;
            }

            // Configuraciones del gráfico
            const width = 600;
            const height = 400;
            const margin = { top: 20, right: 20, bottom: 100, left: 60 };

            // Limpiar el contenido previo
            const svg = d3.select('#chart').selectAll('*').remove();

            // Crear un SVG para el gráfico
            const svgElement = d3.select('#chart').append('svg')
                .attr('width', '100%')
                .attr('height', height)
                .attr('viewBox', `0 0 ${width} ${height}`)
                .attr('preserveAspectRatio', 'xMidYMid meet');

            const chartWidth = width - margin.left - margin.right;
            const chartHeight = height - margin.top - margin.bottom;

            const g = svgElement.append('g')
                .attr('transform', `translate(${margin.left},${margin.top})`);

            // Escalas
            const xScale = d3.scaleBand()
                .domain(values.map(d => d.label)) 
                .range([0, chartWidth])
                .padding(0.1);

            const yScale = d3.scaleLinear()
                .domain([0, d3.max(values, d => d.value)]) 
                .nice()
                .range([chartHeight, 0]);

            // Colores
            const colorScale = d3.scaleOrdinal(d3.schemeCategory10)
                .domain(values.map(d => d.id)); 

            // Crear las barras
            g.selectAll('.bar')
                .data(values)
                .enter().append('rect')
                .attr('class', 'bar')
                .attr('x', d => xScale(d.label))
                .attr('y', d => yScale(d.value))
                .attr('width', xScale.bandwidth())
                .attr('height', d => chartHeight - yScale(d.value))
                .attr('fill', d => colorScale(d.id));

            // Ejes
            g.append('g')
                .attr('class', 'x-axis')
                .attr('transform', `translate(0,${chartHeight})`)
                .call(d3.axisBottom(xScale))
                .selectAll('text')
                .attr('transform', 'rotate(-45)')
                .style('text-anchor', 'end'); 

            g.append('g')
                .attr('class', 'y-axis')
                .call(d3.axisLeft(yScale));

            // Nombres de los ejes
            svgElement.append('text')
                .attr('class', 'x-axis-label')
                .attr('x', width / 2)
                .attr('y', height - margin.bottom / 2 + 20)
                .attr('text-anchor', 'middle')
                .text('Employee First Name');

            svgElement.append('text')
                .attr('class', 'y-axis-label')
                .attr('transform', 'rotate(-90)')
                .attr('x', -margin.top)
                .attr('y', margin.left / 2 - 20)
                .attr('text-anchor', 'middle')
                .text('Count');
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
});