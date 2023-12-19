import React from 'react';
import './DataTable.css'

export default function DataTable(props) {

    let craneArray = [];
    Object.values(props.craneInfo).flat().forEach(item => {
        Object.entries(item).forEach(([k, v]) => {
            craneArray.push(k + v)
        })
    });

    console.log(craneArray);
    console.log(props.craneInfo);
    console.log(Object.keys(props.craneInfo));
    console.log(Object.values(props.craneInfo)); // hij is nested lol

    return (
    <>
    <div className='datatable_container'>
        <table className='datatable_table'>
            <tbody>
                <tr>
                    <th>table</th>
                </tr>
                {craneArray.map(key => {
                    return (
                        <tr key={key}>
                            <td>{key}</td>
                        </tr>
                    )
                })}
            </tbody>
        </table>
        </div>
    </>
    )
}
