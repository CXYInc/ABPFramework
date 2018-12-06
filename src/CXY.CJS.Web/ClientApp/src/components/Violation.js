import React, { Component } from 'react';

export class Violation extends Component {
    displayName = Violation.name

    render() {
        return (
            <div>
                <h1>违章查询</h1>
                <div>
                    车牌号码： <input id="CarNumber" value="" />
                </div>
                <div>
                    车架号码： <input id="CarCode" value="" />
                </div>
                <div>
                    发动机号： <input id="CarEngino" value="" />
                </div>
                <div>
                    <button value="提交" />
                </div>
            </div>
        );
    }
}
