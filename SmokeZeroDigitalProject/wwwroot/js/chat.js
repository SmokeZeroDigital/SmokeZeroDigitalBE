document.addEventListener("DOMContentLoaded", () => {
    const config = window.chatConfig;
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hubs/chat")
        .configureLogging(signalR.LogLevel.Information)
        .build();


    const messageList = document.getElementById("messageList");
    const messageInput = document.getElementById("messageInput");
    const sendForm = document.getElementById("sendMessageForm");

    connection.on("ReceiveMessage", (message) => {
        appendMessage(message);
    });

    function appendMessage(message) {
        const currentUserId = window.chatConfig.senderUserId.toLowerCase();
        const isMe = message.senderUserId.toLowerCase() === currentUserId;

        const alignment = isMe ? "justify-content-end" : "justify-content-start";
        const bubbleClass = isMe ? "bg-primary text-white" : "bg-light border";
        const textAlign = isMe ? "text-end" : "text-start";

        const createdAt = new Date(message.createdAt);
        const formattedTime = createdAt.toLocaleTimeString([], {
            hour: "2-digit",
            minute: "2-digit",
            hour12: false
        });

        const messageHtml = `
        <div class="mb-2 d-flex ${alignment}">
            <div class="px-3 py-2 rounded-4 ${bubbleClass} shadow-sm" style="max-width: 75%;">
                <div class="${textAlign}">${message.content}</div>
                <small class="text-muted d-block ${textAlign}" style="font-size: 0.75rem;">
                    ${formattedTime}
                </small>
            </div>
        </div>
    `;

        document.getElementById("messageList").insertAdjacentHTML("beforeend", messageHtml);
        scrollToBottom();
    }

    function scrollToBottom() {
        const messageList = document.getElementById("messageList");
        messageList.scrollTop = messageList.scrollHeight;
    }



    connection.start().then(() => {
        console.log("✅ SignalR connected");
        connection.invoke("JoinConversation", config.conversationId);
    }).catch(err => console.error("SignalR error:", err));

    sendForm.addEventListener("submit", (e) => {
        e.preventDefault();
        const content = messageInput.value.trim();
        if (!content) return;

        fetch("/api/Chat/send", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                conversationId: config.conversationId,
                senderUserId: config.senderUserId,
                coachId: config.coachId,
                content: content,
                messageType: "TEXT"
            })
        });

        messageInput.value = "";
    });
});
