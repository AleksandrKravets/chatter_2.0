import React from 'react';
import classNames from 'classnames';
import { Button, Layout } from 'antd';

import { ConnectionState, Header as ChatHeader, Message, MessageInput, Spinner, TypingUsers } from '../../components';
import { MessageType } from '../../redux/messages-reducer';
import { MutedUserType } from '../../redux/users-reducer';

import styles from './chat.module.scss';

const { Header, Footer, Content } = Layout;

type Props = {
    isUserLoading: boolean
    areMutedUsersLoaded: boolean
    messages: Array<MessageType>
    mutedUsers: Array<MutedUserType>
    isPrepending: boolean
    nextPageExists: boolean
    currentUserId: number | null
    typingUsers: Array<string>
    isTyping: boolean
    isConnected: boolean
    onLoadMoreMessages: () => void
    onTyping: () => void
}

const Chat: React.FC<Props> = ({
    isUserLoading,
    areMutedUsersLoaded,
    messages,
    mutedUsers,
    isPrepending,
    nextPageExists,
    currentUserId,
    typingUsers,
    isTyping,
    isConnected,
    onLoadMoreMessages,
    onTyping
}) => {
    const messagesList = messages && messages.map((x: MessageType) =>
        <div key={x.id} className={classNames(styles['chat-page__message'], {
            [styles['chat-page__message_right']]: x.owner.id === currentUserId,
            [styles['chat-page__message_left']]: !(x.owner.id === currentUserId)
        })}>
            <Message
                id={x.id}
                text={x.text}
                creationTime={x.creationTime}
                ownerNickname={x.owner.nickname}
                isCurrentUserMessage={x.owner.id === currentUserId}
                isUpdated={x.isUpdated}
                isReply={x.isReply}
                isReplyToCurrentUser={x.messageToReplyData?.ownerId === currentUserId}
                isMuted={mutedUsers.some(y => y.id === x.owner.id)}
                messageToReplyOwnerNickname={x.messageToReplyData?.ownerNickname}
                messageToReplyText={x.messageToReplyData?.text}
            />
        </div>
    )

    return (
        <div className={styles['chat-page']}>
            <Layout >

                <Header>
                    <ChatHeader />
                </Header>

                <Content>
                    <div className={styles['chat-page__messages']}>
                        <ConnectionState isConnected={isConnected} />
                        {
                            <div className={styles['chat-page__container']}>
                                {
                                    !areMutedUsersLoaded || isUserLoading || (isUserLoading && isPrepending)
                                        ? <Spinner isFullPage />
                                        : <>
                                            <MessageLoading 
                                                areMessagesLoading={isPrepending} 
                                                nextPageExists={nextPageExists} 
                                                onLoadMoreBtnClick={onLoadMoreMessages} 
                                            />
                                            {messagesList}
                                            <TypingUsers 
                                                isTyping={isTyping} 
                                                nicknames={typingUsers} 
                                            />
                                        </>
                                }

                            </div>
                        }
                    </div>
                </Content>
                <Footer>
                    <div className={styles['chat-page__footer']}>
                        <div className={styles['chat-page__container']}>
                            <MessageInput onTyping={onTyping} />
                        </div>
                    </div>
                </Footer>

            </Layout>
        </div>
    )
}

type MessageLoadingProps = {
    areMessagesLoading: boolean
    nextPageExists: boolean
    onLoadMoreBtnClick: () => void
}

const MessageLoading: React.FC<MessageLoadingProps> = ({
    areMessagesLoading,
    nextPageExists,
    onLoadMoreBtnClick
}) => {
    return (
        <div className={styles['message-loading']}>
            {
                areMessagesLoading && <Spinner isFullPage={false} />
            }
            {
                !areMessagesLoading && nextPageExists && <Button onClick={onLoadMoreBtnClick} type="primary" size="large" shape="circle">
                    <span className={styles['load-more-messages-btn']}>
                        <i className="fas fa-arrow-up"></i>
                    </span>
                </Button>
            }
        </div>
    )
}


export default Chat;