import React from 'react';
import './DataTable.css'

export default function DataTable(props) {

    return (
        <>
            {console.log(props.craneInfo)}
            <div className='datatable_container'>
                <table className='datatable_table'>
                    <tbody>
                        <tr>
                            <th>table</th>
                        </tr>
                        {Object.keys(props.craneInfo).map(key => {
                            return (
                                <tr key={key}>
                                    <td>{key}</td>
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
