import React from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { AppStateType } from '../../redux/store';
import { fetchMessages, MessageType } from '../../redux/messages-reducer';
import { getToken } from '../../utils/auth/auth-provider';
import { fetchUser } from '../../redux/user-reducer';
import { fetchMutedUsers, fetchOnlineUsersCount } from '../../redux/users-reducer';
import { actions as messagesActions } from './../../redux/messages-reducer';
import { actions as usersActions } from './../../redux/users-reducer';
import { useSignalR } from '../../hooks';
import Chat from './chat';
import { Spinner } from '../../components';

const {
    createMessage,
    updateMessage,
    deleteMessage
} = messagesActions

const {
    setOnlineUsersCount
} = usersActions

type TypingUser = {
    id: number
    nickname: string
    timeoutId: NodeJS.Timeout
}

const ChatContainer = () => {
    const isUserLoading = useSelector((state: AppStateType) => state.user.isLoading);
    const areMutedUsersLoaded = useSelector((state: AppStateType) => state.users.areMutedUsersLoaded);
    const messages = useSelector((state: AppStateType) => state.messages.messages)
    const mutedUsers = useSelector((state: AppStateType) => state.users.mutedUsers);
    const isPrepending = useSelector((state: AppStateType) => state.messages.isPrepending);
    const minMessageId = useSelector((state: AppStateType) => state.messages.minMessageId);
    const nextPageExists = useSelector((state: AppStateType) => state.messages.nextPageExists);
    const currentUser = useSelector((state: AppStateType) => state.user.user);

    const [
        isConnected,
        start,
        stop,
        send
    ] = useSignalR();

    const dispatch = useDispatch();

    const [typingUsers, setTypingUsers] = React.useState<TypingUser[]>([]);

    const handleIsTyping = (userId: number, nickname: string) => {
        const timeoutId = setTimeout(() => {
            setTypingUsers(prev => {
                return [...prev.filter(x => x.id != userId)]
            });

        }, 7000);

        const user: TypingUser = { id: userId, nickname, timeoutId }

        setTypingUsers(prev => {
            prev.forEach(x => x.id === userId && clearInterval(x.timeoutId))
            return [...prev.filter(x => x.id !== user.id), user]
        });
    };

    React.useEffect(() => {
        const startConnection = async () => {
            const token = await getToken()

            await start(`http://localhost:5000/notifications?token=${token}`, [
                [
                    'Create',
                    (message: MessageType) => {
                        dispatch(createMessage(message));
                        setTypingUsers(prev => prev.filter(x => x.id !== message.owner.id))
                    }
                ],
                [
                    'Delete',
                    (id: number) => {
                        dispatch(deleteMessage(id));
                    }
                ],
                [
                    'Update',
                    ({ id, text }: { id: number, text: string }) => {
                        dispatch(updateMessage(id, text));
                    }
                ],
                [
                    'Typing',
                    ({ userId, nickname }) => {
                        handleIsTyping(userId, nickname);
                    }
                ],
                [
                    'UserJoinedChat',
                    ({ id, nickname, onlineUsersCount }: { id: number, nickname: string, onlineUsersCount: number }) => {
                        dispatch(setOnlineUsersCount(onlineUsersCount));
                        console.log('User joined ', id, ' ', nickname, " ", onlineUsersCount)
                    }
                ],
                [
                    'UserLeftChat',
                    ({ id, nickname, onlineUsersCount }: { id: number, nickname: string, onlineUsersCount: number }) => {
                        dispatch(setOnlineUsersCount(onlineUsersCount));
                        console.log('User left ', id, ' ', nickname, " ", onlineUsersCount)
                    }
                ]
            ]);

            dispatch(fetchOnlineUsersCount());
        }

        startConnection();

        return () => {
            console.log('Chat unmounted')
            stop();
        }
    }, []);

    React.useEffect(() => {
        dispatch(fetchUser())
        dispatch(fetchMessages())
        dispatch(fetchMutedUsers());
    }, [])

    const onLoadMoreMessages = () => dispatch(fetchMessages(minMessageId))

    const onTyping = () => send('MessageTyping')

    const appInitialized = currentUser !== null && !isUserLoading && areMutedUsersLoaded
    return (
        <>
            {
                appInitialized
                    ? <Chat
                        isConnected={isConnected}
                        isUserLoading={isUserLoading}
                        areMutedUsersLoaded={areMutedUsersLoaded}
                        messages={messages}
                        mutedUsers={mutedUsers}
                        isPrepending={isPrepending}
                        nextPageExists={nextPageExists}
                        currentUserId={currentUser.id}
                        onLoadMoreMessages={onLoadMoreMessages}
                        onTyping={onTyping}
                        typingUsers={typingUsers.map(x => x.nickname)}
                        isTyping={typingUsers.length > 0}
                    />
                    : <Spinner isFullPage={false} />
            }
        </>

    )
}

export default ChatContainer;