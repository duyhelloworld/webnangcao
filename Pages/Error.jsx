import React from 'react';

class Error extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            requestId: '',
            showRequestId: false
        };
    }

    componentDidMount() {
        const requestId = this.props.requestId || '';
        const showRequestId = requestId !== '';
        this.setState({ requestId, showRequestId });
    }

    render() {
        return (
            <div>
                <h1 className="text-danger">Error.</h1>
                <h2 className="text-danger">An error occurred while processing your request.</h2>

                {this.state.showRequestId && (
                    <p>
                        <strong>Request ID:</strong> <code>{this.state.requestId}</code>
                    </p>
                )}

                <h3>Development Mode</h3>
                <p>
                    Swapping to the <strong>Development</strong> environment displays detailed information about the error that occurred.
                </p>
                <p>
                    <strong>The Development environment shouldn't be enabled for deployed applications.</strong>
                    It can result in displaying sensitive information from exceptions to end users.
                    For local debugging, enable the <strong>Development</strong> environment by setting the <strong>ASPNETCORE_ENVIRONMENT</strong> environment variable to <strong>Development</strong>
                    and restarting the app.
                </p>
            </div>
        );
    }
}

export default Error;