function toggleChat() {
  const chat = document.getElementById("chatPopup");
  chat.style.display = chat.style.display === "flex" ? "none" : "flex";
}

function toggleExpand() {
  const chat = document.getElementById("chatPopup");
  chat.classList.toggle("expanded");
}

function sendMessage() {
  const input = document.getElementById("input");
  const message = input.value.trim();
  if (!message) return;

  const messages = document.getElementById("messages");

  const userMsg = document.createElement("div");
  userMsg.className = "message user";
  userMsg.textContent = message;
  messages.appendChild(userMsg);

  input.value = "";
  messages.scrollTop = messages.scrollHeight;

  setTimeout(() => {
    const botMsg = document.createElement("div");
    botMsg.className = "message bot";
    botMsg.textContent = "Message reçu 👍";
    messages.appendChild(botMsg);
    messages.scrollTop = messages.scrollHeight;
  }, 600);
}