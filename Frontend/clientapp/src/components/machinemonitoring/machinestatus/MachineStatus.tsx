import * as d3 from 'd3';
import { useEffect } from 'react';
import './MachineStatus.scss'

interface IMachineStatus {
    uptime: boolean[],
    name: string
}

export default function MachineStatus(props: IMachineStatus) {
    useEffect(() => {
        let div = d3.select(`#${props.name}`)
            .append('div')
            .attr('height', 20)
            .attr('width', '100%')
        ;

        div.selectAll('div')
            .data(props.uptime)
            .enter()
            .append('div')
            .attr('class', (data) => {
                return data ? 'Good' : 'Bad'
            })
        ;
        
        // bar.append('div')
        //     .attr('class', (d) => {
        //         return d ? 'Good' : 'Bad'
        //     })
        //     .attr('width', 45)
        //     .attr('height', 20)
        // ;
    }, []);

    return (
        <div id={props.name} className='MM-Uptime'>
            {/* {props.uptime.map((value: boolean, index: number) => {
                return (
                    <div key={index} className={ value ? 'good' : 'bad' } />
                )
            })} */}
        </div>
    )
}
