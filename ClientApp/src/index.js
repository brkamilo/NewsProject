import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import  Button  from '@material-ui/core/Button';

//import registerServiceWorker from './registerServiceWorker';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

function Init() {
    return (
        <Button variant="contained" color="primary">
            Hola Mundo!
        </Button>
    );
}


class WeatherInfo extends React.Component {

    constructor() {
        super();
        this.state = {
            items: [],
            isLoaded: false
        }
    }
    componentDidMount() {
        fetch(`https://api.openweathermap.org/data/2.5/forecast?q=Austin,USA&appid=583f803dfc6a7f2f96ff9957c330c2b0&units=imperial`)
            .then(results => results.json())
            .then(json => {
                this.setState({
                    isLoaded: true,
                    items: json
                })
            });
    }

    render() {

        let {
            isLoaded,
            items
        } = this.state;
        if (!isLoaded) {
            return (<div> Loading... </div>)
        } else {
            return (<div>
                <ul>
                    {items.list.map((item, key) => (
                        <li key="{key}">
                            Temperatura: {item.main.temp}
                        </li>
                    ))}
                </ul>
            </div>
            );
        }
    }
}




ReactDOM.render(
    <BrowserRouter basename={baseUrl}>
        <App />,  
        <WeatherInfo name="World" />

        
    </BrowserRouter>,
    rootElement)  ;

// Uncomment the line above that imports the registerServiceWorker function
// and the line below to register the generated service worker.
// By default create-react-app includes a service worker to improve the
// performance of the application by caching static assets. This service
// worker can interfere with the Identity UI, so it is
// disabled by default when Identity is being used.
//
//registerServiceWorker();

