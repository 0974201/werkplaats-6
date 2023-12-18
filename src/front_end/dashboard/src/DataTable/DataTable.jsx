import React from 'react';
import './DataTable.css'

export default function DataTable(props) {

    return (
        <>
            {console.log(props)}
            <div className='datatable_container'>
                <table className='datatable_table'>
                    <tbody>
                        <tr>
                            <th>table</th>
                        </tr>
                        <tr key={props.craneInfo}>
                            <td>{props.craneInfo}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </>
    )
}