import React from 'react';
import ChatService from '../utils/signalR/chat-service';

const useSignalR = () : [
    boolean, 
    (url: string, handlers: Array<[string, (...handler: any[]) => void]>) => Promise<void>, 
    () => void,
    (name: string, ...args: any) => void
] => {
    const service = React.useRef<null | ChatService>(null);
    const [isConnected, setIsConnected] = React.useState(false);

    const start = async (url: string, handlers: Array<[string, (...handler: any[]) => void]>) : Promise<void> => {
        service.current = new ChatService(url); 

        handlers.forEach(x => {
            const [type, handler] = [...x];
            service.current?.register(type, handler)
        })

        service.current.subscribeOnConnectionStateChange((isConnected) => {
            setIsConnected(isConnected);
        })

        await service.current?.start();
    }

    const stop = () => {
        service.current?.stop();
    }

    const send = (name: string, ...args: Array<any>) => {
        service.current?.send(name, ...args);
    }

    return [
        isConnected,
        start,
        stop,
        send
    ]
}

export default useSignalR;