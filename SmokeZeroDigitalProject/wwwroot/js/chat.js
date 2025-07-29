document.addEventListener("DOMContentLoaded", () => {
    console.log("📦 DOM fully loaded");

    const config = window.chatConfig;
    console.log("🛠️ chatConfig:", config);

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hubs/chat")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    const messageList = document.getElementById("messageList");
    const messageInput = document.getElementById("messageInput");
    const sendForm = document.getElementById("sendMessageForm");

    if (!messageList) console.error("❌ Không tìm thấy messageList (#messageList)");
    if (!messageInput) console.error("❌ Không tìm thấy messageInput (#messageInput)");
    if (!sendForm) console.error("❌ Không tìm thấy sendForm (#sendMessageForm)");

    function scrollToBottom() {
        if (messageList) {
            messageList.scrollTop = messageList.scrollHeight;
        }
    }

    function appendMessage(message) {
        const isMe = message.senderUserId.toLowerCase() === config.senderUserId.toLowerCase();
        const alignment = isMe ? "justify-content-end" : "justify-content-start";
        const bubbleClass = isMe ? "bg-primary text-white" : "bg-light border";
        const textAlign = isMe ? "text-end" : "text-start";

        const createdAt = new Date(message.createdAt).toLocaleTimeString([], {
            hour: "2-digit", minute: "2-digit", hour12: false
        });

        const html = `
        <div class="mb-2 d-flex ${alignment}">
            <div class="px-3 py-2 rounded-4 ${bubbleClass} shadow-sm" style="max-width: 75%;">
                <div class="${textAlign}">${message.content}</div>
                <small class="text-muted d-block ${textAlign}" style="font-size: 0.75rem;">${createdAt}</small>
            </div>
        </div>`;

        messageList.insertAdjacentHTML("beforeend", html);
        setTimeout(scrollToBottom, 50);

        const conversationItem = document.querySelector(`.conversation-item[data-id="${message.conversationId}"]`);
        if (conversationItem) {
            const lastMessageDiv = conversationItem.querySelector(".last-message");
            if (lastMessageDiv) lastMessageDiv.textContent = message.content;

            conversationItem.classList.add("bg-light");
            setTimeout(() => conversationItem.classList.remove("bg-light"), 5000);
        }
    }

    connection.on("ReceiveMessage", (message) => {
        console.log("📩 ReceiveMessage:", message);
        appendMessage(message);
    });

    connection.start()
        .then(() => {
            console.log("✅ SignalR connected");
            return connection.invoke("JoinConversation", config.conversationId);
        })
        .then(() => {
            console.log("✅ Joined conversation group:", config.conversationId);
            setTimeout(scrollToBottom, 100);
        })
        .catch((err) => console.error("❌ SignalR connection error:", err));

    sendForm.addEventListener("submit", async (e) => {
        e.preventDefault();
        const content = messageInput.value.trim();
        if (!content) return;

        const message = {
            conversationId: config.conversationId,
            senderUserId: config.senderUserId,
            coachId: config.coachId,
            content: content,
            messageType: "TEXT",
            createdAt: new Date().toISOString()
        };

        try {
            await fetch("/api/Chat/send", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(message)
            });
            messageInput.value = "";
        } catch (err) {
            console.error("❌ Error sending message:", err);
        }
    });
});
