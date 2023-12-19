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
                        {Object.keys(props.list).map(key => {
                            return (
                                <tr key={key}>
                                    <td>{key}</td>
                                    <td>{props.list[key].craneInfo}</td>
                                </tr>
                            )
                        })}
                    </tbody>
                </table>
            </div>
        </>
    )
}
